﻿using System.Windows.Media;
using AudioBand.Models;
using Nett;

namespace AudioBand.Settings
{
    /// <summary>
    /// Helper for TOML serialization and deserialization.
    /// </summary>
    internal static class TomlHelper
    {
        /// <summary>
        /// Gets the default settings for TOML de/serialization.
        /// </summary>
        public static TomlSettings DefaultSettings { get; } = TomlSettings.Create(cfg =>
        {
            cfg.ConfigureType<Color>(type => type.WithConversionFor<TomlString>(convert => convert
                .ToToml(SerializationConversions.ColorToString)
                .FromToml(tomlString => SerializationConversions.StringToColor(tomlString.Value))));
            cfg.ConfigureType<CustomLabel.TextAlignment>(type => type.WithConversionFor<TomlString>(convert => convert
                .ToToml(SerializationConversions.EnumToString)
                .FromToml(str => SerializationConversions.StringToEnum<CustomLabel.TextAlignment>(str.Value))));
            cfg.ConfigureType<double>(type => type.WithConversionFor<TomlInt>(c => c
                .FromToml(tml => tml.Value)));
        });
    }
}