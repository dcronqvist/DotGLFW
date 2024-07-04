ifeq ($(OS),Windows_NT)
SHELL := pwsh.exe
.SHELLFLAGS := -NoProfile -Command
endif

# SOURCE FILES
SOURCES := $(wildcard DotGLFW/GLFW/*.cs)
SOURCES +=  DotGLFW/DotGLFW.csproj

# DOTGLFW VERSION
DOTGLFW_VERSION := 1.1.0

# VERSION OF GLFW TO DOWNLOAD
GLFW_VERSION := 3.4

# BUILD STUFF
TMP_DIR := .tmp
RANDOM_FILE := $(shell pwsh -c "Write-Host $$(New-TemporaryFile)")
RUNTIMEFOLDER_WINX64 := DotGLFW/runtimes/win-x64/native
RUNTIMEFOLDER_WINX86 := DotGLFW/runtimes/win-x86/native
RUNTIMEFOLDER_OSXX64 := DotGLFW/runtimes/osx-x64/native
RUNTIMEFOLDER_OSXARM64 := DotGLFW/runtimes/osx-arm64/native
NUPKGFILE := nupkg/DotGLFW.$(DOTGLFW_VERSION).nupkg

# FUNCTIONS
# call rmrf,<path>
define rmrf
	if (Test-Path $(1)) { Remove-Item -Recurse -Force -Path $(1) }
endef

# call download_zip_and_extract_file,<url>,<path-inside-zip-to-file>,<destination-dir>,<output-file-name>
define download_zip_and_extract_file
	@echo "Downloading $(1)"
	@New-Item -ItemType Directory -Force -Path "$(3)" | Out-Null
	@if(Test-Path "$(3)/$(4)") { Remove-Item -Recurse -Force -Path "$(3)/$(4)" }
	@Invoke-WebRequest -Uri "$(1)" -OutFile "$(RANDOM_FILE)"
	@Expand-Archive -Path "$(RANDOM_FILE)" -DestinationPath "$(TMP_DIR)"
	@Copy-Item -Path "$(TMP_DIR)/$(2)" -Destination "$(3)/$(4)"
	@Remove-Item -Recurse -Force -Path "$(RANDOM_FILE)"
	@Remove-Item -Recurse -Force -Path "$(TMP_DIR)"
	@echo "Downloaded $(3)/$(4)"
endef

.PHONY: clean
clean:
	@echo "Cleaning DotGLFW"
	@${call rmrf,./publish} 
	@${call rmrf,./nupkg} 
	@${call rmrf,./bin} 
	@${call rmrf,./obj} 
	@${call rmrf,./DotGLFW/bin} 
	@${call rmrf,./DotGLFW/obj} 
	@${call rmrf,./DotGLFW/runtimes} 
	@${call rmrf,./DotGLFW.LocalExample/bin} 
	@${call rmrf,./DotGLFW.LocalExample/obj} 
	@${call rmrf,./DotGLFW.NugetExample/bin} 
	@${call rmrf,./DotGLFW.NugetExample/obj} 

.PHONY: run-local
run-local: $(NUPKGFILE)
	dotnet run --project DotGLFW.LocalExample/DotGLFW.LocalExample.csproj --runtime win-x64

.PHONY: run-nuget
run-nuget: $(NUPKGFILE)
	dotnet run --project DotGLFW.NugetExample/DotGLFW.NugetExample.csproj

.PHONY: debug-local
LOCAL_EXAMPLE_DLL := DotGLFW.LocalExample/bin/Debug/net8.0/win-x64/DotGLFW.LocalExample.dll
debug-local: $(NUPKGFILE)
	dotnet build DotGLFW.LocalExample/DotGLFW.LocalExample.csproj -c Debug --runtime win-x64
	@Start-Process "vscode-insiders://fabiospampinato.vscode-debug-launcher/launch?args=$(shell pwsh .scripts/gen_debug_args.ps1 $(LOCAL_EXAMPLE_DLL) . '[]')"

.PHONY: debug-nuget
debug-nuget: $(NUPKGFILE)
	dotnet build DotGLFW.Example/DotGLFW.Example.csproj -c Debug
	@echo "$(shell pwsh .scripts/gen_debug_args.ps1 $(EXAMPLE_DLL) . '[]')"
	@Start-Process "vscode-insiders://fabiospampinato.vscode-debug-launcher/launch?args=$(shell pwsh .scripts/gen_debug_args.ps1 $(EXAMPLE_DLL) . '[]')"

.PHONY: pack
pack: $(NUPKGFILE)

# NuGet package
$(NUPKGFILE): $(SOURCES) $(RUNTIMEFOLDER_WINX64)/glfw3.dll $(RUNTIMEFOLDER_WINX86)/glfw3.dll $(RUNTIMEFOLDER_OSXX64)/libglfw3.dylib $(RUNTIMEFOLDER_OSXARM64)/libglfw3.dylib
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