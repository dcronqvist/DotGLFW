ifeq ($(OS),Windows_NT)
SHELL := pwsh.exe
.SHELLFLAGS := -NoProfile -Command
endif

# SOURCE FILES
SOURCES := $(wildcard DotGLFW/GLFW/*.cs)
SOURCES +=  DotGLFW/DotGLFW.csproj

# DOTGLFW VERSION
DOTGLFW_VERSION := 1.0.0

# VERSION OF GLFW TO DOWNLOAD
GLFW_VERSION := 3.3.8

# BUILD STUFF
RANDOM_FILE := $(shell pwsh -c "Write-Host $$(New-TemporaryFile)")
RUNTIMEFOLDER_WINX64 := DotGLFW/runtimes/win-x64/native
RUNTIMEFOLDER_WINX86 := DotGLFW/runtimes/win-x86/native
RUNTIMEFOLDER_OSXX64 := DotGLFW/runtimes/osx-x64/native
RUNTIMEFOLDER_OSXARM64 := DotGLFW/runtimes/osx-arm64/native
NUPKGFILE := nupkg/DotGLFW.$(DOTGLFW_VERSION).nupkg

.PHONY: example-publish
example-publish: $(NUPKGFILE)
	dotnet publish DotGLFW.Example/DotGLFW.Example.csproj -c Release --self-contained true -o ./publish/win-x64 -r win-x64 /p:DebugType=None /p:DebugSymbols=false
	dotnet publish DotGLFW.Example/DotGLFW.Example.csproj -c Release --self-contained true -o ./publish/win-x86 -r win-x86 /p:DebugType=None /p:DebugSymbols=false
	dotnet publish DotGLFW.Example/DotGLFW.Example.csproj -c Release --self-contained true -o ./publish/osx-x64 -r osx-x64 /p:DebugType=None /p:DebugSymbols=false
	dotnet publish DotGLFW.Example/DotGLFW.Example.csproj -c Release --self-contained true -o ./publish/osx-arm64 -r osx-arm64 /p:DebugType=None /p:DebugSymbols=false

.PHONY: example-run
example-run: $(NUPKGFILE)
	dotnet run --project DotGLFW.Example/DotGLFW.Example.csproj -c Debug

.PHONY: pack
pack: $(NUPKGFILE)

# PACK
$(NUPKGFILE): $(SOURCES) $(RUNTIMEFOLDER_WINX64)/glfw3.dll $(RUNTIMEFOLDER_WINX86)/glfw3.dll $(RUNTIMEFOLDER_OSXX64)/libglfw3.dylib $(RUNTIMEFOLDER_OSXARM64)/libglfw3.dylib
	@echo "Packing DotGLFW $(DOTGLFW_VERSION)"
	dotnet pack -c Release DotGLFW -o ./nupkg -p:PackageVersion=$(DOTGLFW_VERSION)
	@dotnet nuget locals all --clear
	@dotnet restore DotGLFW.sln

$(RUNTIMEFOLDER_WINX64)/glfw3.dll:
	@echo "Downloading GLFW $(GLFW_VERSION) for Windows x64"
	@New-Item -ItemType Directory -Force -Path "$(RUNTIMEFOLDER_WINX64)" | Out-Null
	@if(Test-Path "$(RUNTIMEFOLDER_WINX64)/glfw3.dll") { Remove-Item -Recurse -Force -Path "$(RUNTIMEFOLDER_WINX64)/glfw3.dll" }
	Invoke-WebRequest -Uri "https://github.com/glfw/glfw/releases/download/$(GLFW_VERSION)/glfw-$(GLFW_VERSION).bin.WIN64.zip" -OutFile "$(RANDOM_FILE)"
	@Expand-Archive -Path "$(RANDOM_FILE)" -DestinationPath "$(RUNTIMEFOLDER_WINX64)"
	Copy-Item -Path "$(RUNTIMEFOLDER_WINX64)/glfw-$(GLFW_VERSION).bin.WIN64/lib-vc2022/glfw3.dll" -Destination "$(RUNTIMEFOLDER_WINX64)/glfw3.dll"
	@Remove-Item -Recurse -Force -Path "$(RANDOM_FILE)"
	@Remove-Item -Recurse -Force -Path "$(RUNTIMEFOLDER_WINX64)/glfw-$(GLFW_VERSION).bin.WIN64"

$(RUNTIMEFOLDER_WINX86)/glfw3.dll:
	@echo "Downloading GLFW $(GLFW_VERSION) for Windows x86"
	@New-Item -ItemType Directory -Force -Path "$(RUNTIMEFOLDER_WINX86)" | Out-Null
	@if(Test-Path "$(RUNTIMEFOLDER_WINX86)/glfw3.dll") { Remove-Item -Recurse -Force -Path "$(RUNTIMEFOLDER_WINX86)/glfw3.dll" }
	Invoke-WebRequest -Uri "https://github.com/glfw/glfw/releases/download/$(GLFW_VERSION)/glfw-$(GLFW_VERSION).bin.WIN32.zip" -OutFile "$(RANDOM_FILE)"
	@Expand-Archive -Path "$(RANDOM_FILE)" -DestinationPath "$(RUNTIMEFOLDER_WINX86)"
	Copy-Item -Path "$(RUNTIMEFOLDER_WINX86)/glfw-$(GLFW_VERSION).bin.WIN32/lib-vc2022/glfw3.dll" -Destination "$(RUNTIMEFOLDER_WINX86)/glfw3.dll"
	@Remove-Item -Recurse -Force -Path "$(RANDOM_FILE)"
	@Remove-Item -Recurse -Force -Path "$(RUNTIMEFOLDER_WINX86)/glfw-$(GLFW_VERSION).bin.WIN32"

$(RUNTIMEFOLDER_OSXX64)/libglfw3.dylib:
	@echo "Downloading GLFW $(GLFW_VERSION) for macOS x64"
	@New-Item -ItemType Directory -Force -Path "$(RUNTIMEFOLDER_OSXX64)" | Out-Null
	@if(Test-Path "$(RUNTIMEFOLDER_OSXX64)/libglfw3.dylib") { Remove-Item -Recurse -Force -Path "$(RUNTIMEFOLDER_OSXX64)/libglfw3.dylib" }
	Invoke-WebRequest -Uri "https://github.com/glfw/glfw/releases/download/$(GLFW_VERSION)/glfw-$(GLFW_VERSION).bin.MACOS.zip" -OutFile "$(RANDOM_FILE)"
	@Expand-Archive -Path "$(RANDOM_FILE)" -DestinationPath "$(RUNTIMEFOLDER_OSXX64)"
	Copy-Item -Path "$(RUNTIMEFOLDER_OSXX64)/glfw-$(GLFW_VERSION).bin.MACOS/lib-universal/libglfw.3.dylib" -Destination "$(RUNTIMEFOLDER_OSXX64)/libglfw3.dylib"
	@Remove-Item -Recurse -Force -Path "$(RANDOM_FILE)"
	@Remove-Item -Recurse -Force -Path "$(RUNTIMEFOLDER_OSXX64)/glfw-$(GLFW_VERSION).bin.MACOS"

$(RUNTIMEFOLDER_OSXARM64)/libglfw3.dylib:
	@echo "Downloading GLFW $(GLFW_VERSION) for macOS ARM64"
	@New-Item -ItemType Directory -Force -Path "$(RUNTIMEFOLDER_OSXARM64)" | Out-Null
	@if(Test-Path "$(RUNTIMEFOLDER_OSXARM64)/libglfw3.dylib") { Remove-Item -Recurse -Force -Path "$(RUNTIMEFOLDER_OSXARM64)/libglfw3.dylib" }
	Invoke-WebRequest -Uri "https://github.com/glfw/glfw/releases/download/$(GLFW_VERSION)/glfw-$(GLFW_VERSION).bin.MACOS.zip" -OutFile "$(RANDOM_FILE)"
	@Expand-Archive -Path "$(RANDOM_FILE)" -DestinationPath "$(RUNTIMEFOLDER_OSXARM64)"
	Copy-Item -Path "$(RUNTIMEFOLDER_OSXARM64)/glfw-$(GLFW_VERSION).bin.MACOS/lib-arm64/libglfw.3.dylib" -Destination "$(RUNTIMEFOLDER_OSXARM64)/libglfw3.dylib"
	@Remove-Item -Recurse -Force -Path "$(RANDOM_FILE)"
	@Remove-Item -Recurse -Force -Path "$(RUNTIMEFOLDER_OSXARM64)/glfw-$(GLFW_VERSION).bin.MACOS"