namespace Mffm.Contracts;

using System;
/// <summary>
/// Provides the base class for system exceptions.
/// </summary>
public class ServiceNotFoundException(string message) : Exception(message);