// System namespaces
global using System.Diagnostics.CodeAnalysis;
global using System.IO.Compression;
global using System.Text.Json.Serialization;

// Microsoft namespaces
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.ResponseCompression;
global using Microsoft.OpenApi.Models;

// Localiza namespaces
global using Localiza.BuildingBlocks.Logging.Attributes;
global using Localiza.BuildingBlocks.Logging.Configurations;
global using Localiza.BuildingBlocks.Logging.Enums;
global using Localiza.BuildingBlocks.Logging.Models;
global using Localiza.BuildingBlocks.Logging.Services.Interfaces;
global using Localiza.BuildingBlocks.Logging.Adapters;
global using Localiza.BuildingBlocks.Logging.Filters;
global using LogLevel = Localiza.BuildingBlocks.Logging.Enums.LogLevel;

// Third Party Libs namespaces
global using BuildingBlock.CorrelationId.ServiceCollectionExtensions;

// Solution-specific namespaces
global using FI.Recebimento.Publicador.ACL.Api.Extensions;