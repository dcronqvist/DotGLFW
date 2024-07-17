# Platform-specific variables
# RANDOM_FILE
# rmrf <path>
# mkdir <path>
# fetch <url> <output-file>
# xzip <zip-file> <destination-dir>
# cp <source> <destination>
# gen_debug_args <dll> <cwd> <args>s

ifeq ($(OS),Windows_NT)
	SHELL := pwsh.exe
	.SHELLFLAGS := -NoProfile -Command

	RANDOM_FILE := $(shell pwsh -c "Write-Host $$(New-TemporaryFile)")
define rmrf
	if (Test-Path $(1)) { Remove-Item -Recurse -Force -Path $(1) }
endef
define mkdir
	New-Item -ItemType Directory -Force -Path $(1) | Out-Null
endef
define fetch
	Invoke-WebRequest -Uri $(1) -OutFile $(2)
endef
define xzip
	Expand-Archive -Path $(1) -DestinationPath $(2)
endef
define cp
	Copy-Item -Path $(1) -Destination $(2)
endef
define gen_debug_args
	$(shell pwsh .scripts/gen_debug_args.ps1 $(1) $(2) $(3))
endef

else
	UNAME_S := $(shell uname -s)
	ifeq ($(UNAME_S),Darwin)
		SHELL := pwsh
		.SHELLFLAGS := -NoProfile -Command

		RANDOM_FILE := $(shell pwsh -c "Write-Host $$(New-TemporaryFile)")
		GREP := grep
define rmrf
	if (Test-Path $(1)) { Remove-Item -Recurse -Force -Path $(1) }
endef
define mkdir
	New-Item -ItemType Directory -Force -Path $(1) | Out-Null
endef
define fetch
	Invoke-WebRequest -Uri $(1) -OutFile $(2)
endef
define xzip
	Expand-Archive -Path $(1) -DestinationPath $(2)
endef
define cp
	Copy-Item -Path $(1) -Destination $(2)
endef

    else
        # TODO: Linux support for makefile
    endif
endif

# DOTGLFW VERSION
DOTGLFW_VERSION := 1.1.0

# VERSION OF GLFW TO DOWNLOAD
GLFW_VERSION := 3.4

# BUILD STUFF
TMP_DIR := .tmp
RUNTIMEFOLDER_WINX64 := DotGLFW/runtimes/win-x64/native
RUNTIMEFOLDER_WINX86 := DotGLFW/runtimes/win-x86/native
RUNTIMEFOLDER_OSXX64 := DotGLFW/runtimes/osx-x64/native
RUNTIMEFOLDER_OSXARM64 := DotGLFW/runtimes/osx-arm64/native
NUPKGFILE := nupkg/DotGLFW.$(DOTGLFW_VERSION).nupkg

# call download_zip_and_extract_file,<url>,<path-inside-zip-to-file>,<destination-dir>,<output-file-name>
define download_zip_and_extract_file
	@echo "Downloading $(1)"
	@${call mkdir,"$(3)"}
	@${call rmrf,"$(3)/$(4)"}
	@${call fetch,"$(1)","$(RANDOM_FILE)"}
	@${call xzip,"$(RANDOM_FILE)","$(TMP_DIR)"}
	@${call cp,"$(TMP_DIR)/$(2)","$(3)/$(4)"}
	@${call rmrf,"$(RANDOM_FILE)"}
	@${call rmrf,"$(TMP_DIR)"}
	@echo "Downloaded $(3)/$(4)"
endef

.PHONY: clean
clean:
	@echo "Cleaning DotGLFW"
	@${call rmrf,./.glfw} 
	@${call rmrf,./publish} 
	@${call rmrf,./nupkg} 
	@${call rmrf,./bin} 
	@${call rmrf,./obj} 
	@${call rmrf,./DotGLFW/bin} 
	@${call rmrf,./DotGLFW/obj} 
	@${call rmrf,./DotGLFW/Generated} 
	@${call rmrf,./DotGLFW/runtimes} 
	@${call rmrf,./DotGLFW.LocalExample/bin} 
	@${call rmrf,./DotGLFW.LocalExample/obj} 
	@${call rmrf,./DotGLFW.NugetExample/bin} 
	@${call rmrf,./DotGLFW.NugetExample/obj} 
	@${call rmrf,./DotGLFW.Generator/bin} 
	@${call rmrf,./DotGLFW.Generator/obj} 

.PHONY: run-local
run-local: $(NUPKGFILE)
	dotnet run --project DotGLFW.LocalExample/DotGLFW.LocalExample.csproj

.PHONY: run-nuget
run-nuget: $(NUPKGFILE)
	dotnet run --project DotGLFW.NugetExample/DotGLFW.NugetExample.csproj

.PHONY: debug-local
LOCAL_EXAMPLE_DLL := DotGLFW.LocalExample/bin/Debug/net8.0/DotGLFW.LocalExample.dll
debug-local: $(NUPKGFILE)
	dotnet build DotGLFW.LocalExample/DotGLFW.LocalExample.csproj -c Debug
	@Start-Process "vscode-insiders://fabiospampinato.vscode-debug-launcher/launch?args=$(shell pwsh .scripts/gen_debug_args.ps1 $(LOCAL_EXAMPLE_DLL) . '[]')"

.PHONY: debug-nuget
debug-nuget: $(NUPKGFILE)
	dotnet build DotGLFW.Example/DotGLFW.Example.csproj -c Debug
	@echo "$(shell pwsh .scripts/gen_debug_args.ps1 $(EXAMPLE_DLL) . '[]')"
	@Start-Process "vscode-insiders://fabiospampinato.vscode-debug-launcher/launch?args=$(shell pwsh .scripts/gen_debug_args.ps1 $(EXAMPLE_DLL) . '[]')"

.PHONY: run-generator
run-generator: DotGLFW/Generated

.PHONY: debug-generator
GENERATOR_DEBUG_DLL := DotGLFW.Generator/bin/Debug/net8.0/DotGLFW.Generator.dll
debug-generator: 
	dotnet build DotGLFW.Generator/DotGLFW.Generator.csproj -c Debug
	@Start-Process "vscode-insiders://fabiospampinato.vscode-debug-launcher/launch?args=$(shell pwsh .scripts/gen_debug_args.ps1 $(GENERATOR_DEBUG_DLL) . '[]')"

.PHONY: pack
pack: $(NUPKGFILE)

GENERATOR_SOURCES := $(wildcard DotGLFW.Generator/*.cs) $(wildcard DotGLFW.Generator/Generation/*.cs) $(wildcard DotGLFW.Generator/Model/*.cs) $(wildcard DotGLFW.Generator/Parsing/*.cs) DotGLFW.Generator/DotGLFW.Generator.csproj
DotGLFW/Generated: .glfw/glfw-$(GLFW_VERSION) $(GENERATOR_SOURCES)
	dotnet run --project DotGLFW.Generator/DotGLFW.Generator.csproj -- .glfw/glfw-$(GLFW_VERSION) DotGLFW/Generated LICENSE "https://www.glfw.org/docs/3.4/"

.glfw/glfw-%:
	@echo "Downloading GLFW $* from https://github.com/glfw/glfw/releases/download/$*/glfw-$*.zip"
	@${call mkdir,".glfw"}
	@${call fetch,"https://github.com/glfw/glfw/releases/download/$*/glfw-$*.zip",".glfw/glfw-$*.zip"}
	@${call xzip,".glfw/glfw-$*.zip",".glfw"}
	@${call rmrf,".glfw/glfw-$*.zip"}

# NuGet package
DOTGLFW_SOURCES := DotGLFW/Generated $(wildcard DotGLFW/Generated/*.cs) $(wildcard DotGLFW/GLFW/*.cs) DotGLFW/DotGLFW.csproj
$(NUPKGFILE): $(DOTGLFW_SOURCES) $(RUNTIMEFOLDER_WINX64)/glfw3.dll $(RUNTIMEFOLDER_WINX86)/glfw3.dll $(RUNTIMEFOLDER_OSXX64)/libglfw3.dylib $(RUNTIMEFOLDER_OSXARM64)/libglfw3.dylib
	@echo "Packing DotGLFW $(DOTGLFW_VERSION) into nupkg/DotGLFW.$(DOTGLFW_VERSION).nupkg"
	dotnet pack -c Release DotGLFW -o ./nupkg -p:PackageVersion=$(DOTGLFW_VERSION) -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
	@dotnet nuget locals all --clear
	@dotnet restore DotGLFW.sln

# Native dependencies + download
WINX64_URL := https://github.com/glfw/glfw/releases/download/$(GLFW_VERSION)/glfw-$(GLFW_VERSION).bin.WIN64.zip
WINX64_DLL_IN_ZIP := glfw-$(GLFW_VERSION).bin.WIN64/lib-vc2022/glfw3.dll
$(RUNTIMEFOLDER_WINX64)/glfw3.dll:
	$(call download_zip_and_extract_file,$(WINX64_URL),$(WINX64_DLL_IN_ZIP),$(RUNTIMEFOLDER_WINX64),glfw3.dll)

WINX86_URL := https://github.com/glfw/glfw/releases/download/$(GLFW_VERSION)/glfw-$(GLFW_VERSION).bin.WIN32.zip
WINX86_DLL_IN_ZIP := glfw-$(GLFW_VERSION).bin.WIN32/lib-vc2022/glfw3.dll
$(RUNTIMEFOLDER_WINX86)/glfw3.dll:
	$(call download_zip_and_extract_file,$(WINX86_URL),$(WINX86_DLL_IN_ZIP),$(RUNTIMEFOLDER_WINX86),glfw3.dll)

OSX64_URL := https://github.com/glfw/glfw/releases/download/$(GLFW_VERSION)/glfw-$(GLFW_VERSION).bin.MACOS.zip
OSX64_DYLIB_IN_ZIP := glfw-$(GLFW_VERSION).bin.MACOS/lib-universal/libglfw.3.dylib
$(RUNTIMEFOLDER_OSXX64)/libglfw3.dylib:
	$(call download_zip_and_extract_file,$(OSX64_URL),$(OSX64_DYLIB_IN_ZIP),$(RUNTIMEFOLDER_OSXX64),libglfw3.dylib)

OSXARM64_URL := https://github.com/glfw/glfw/releases/download/$(GLFW_VERSION)/glfw-$(GLFW_VERSION).bin.MACOS.zip
OSXARM64_DYLIB_IN_ZIP := glfw-$(GLFW_VERSION).bin.MACOS/lib-arm64/libglfw.3.dylib
$(RUNTIMEFOLDER_OSXARM64)/libglfw3.dylib:
	$(call download_zip_and_extract_file,$(OSXARM64_URL),$(OSXARM64_DYLIB_IN_ZIP),$(RUNTIMEFOLDER_OSXARM64),libglfw3.dylib)