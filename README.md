# ☕ DotGLFW [![Nuget](https://img.shields.io/nuget/v/DotGLFW)](https://www.nuget.org/packages/DotGLFW) [![Targets](https://img.shields.io/badge/targets-netstandard2.0;net8.0-blue)](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) [![Nuget](https://img.shields.io/nuget/dt/DotGLFW)](https://www.nuget.org/packages/DotGLFW) 

A 1:1 mapping of the native GLFW API for .NET, with a lightweight managed wrapper for easy integration in your projects! It also includes XML docs for all functions and types from the official GLFW documentation. Supports NativeAot, see [this example](DotGLFW.NugetAotExample/).

> [!NOTE]
> Uses the [latest version of GLFW (3.4)](https://github.com/glfw/glfw/releases/tag/3.4).

The library is split into two parts: the *unmanaged API* and the *managed API*.

The unmanaged API is automatically generated using [DotGLFW.Generator](DotGLFW.Generator/). It parses the official GLFW documentation and generates C# code that exactly matches the native API.

The managed API is a hand-written wrapper around the unmanaged API that provides a more user-friendly interface for .NET developers. Parts of the managed API is automatically generated using [DotGLFW.Generator](DotGLFW.Generator/), but only simpler things such as enums and structs.

## Unmanaged API

The unmanaged API is the static `NativeGlfw` class, which has exact mappings for all GLFW macros (e.g. `GLFW_TRUE`) and functions (e.g. `glfwInit`). Typedefs are also mapped to their equivalents in C#, opaque handles are empty structs, and function pointers are represented as delegates.

> [!IMPORTANT]
> The unmanaged API will require you to perform your own marshalling and handle memory management yourself. The managed API has been created to alleviate this.

## Managed API

The managed API is the `Glfw` class, which wraps the unmanaged API in a more user-friendly and .NET-y way. It provides compile-time marshalling of strings, arrays, and other types, and handles memory management for you (including keeping references to callbacks alive). Many functions (e.g. `glfwWindowHint`) have been overloaded to accept more user-friendly enums and types instead of raw integers.

Take a look in [the provided example](DotGLFW.Example/Program.cs) to see how to use the library with the managed API. It demonstrates how to create a window and set up a very simple rendering loop.