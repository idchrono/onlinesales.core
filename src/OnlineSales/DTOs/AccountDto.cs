﻿// <copyright file="AccountDto.cs" company="WavePoint Co. Ltd.">
// Licensed under the MIT license. See LICENSE file in the samples root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace OnlineSales.DTOs;

public class AccountCreateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? City { get; set; }

    public string? StateCode { get; set; }

    public string? Country { get; set; }

    public string? EmployeesRate { get; set; }

    public double? Revenue { get; set; }

    [Required]
    public int DomainId { get; set; }

    public string[] Tags { get; set; } = Array.Empty<string>();

    public Dictionary<string, string>? SocialMedia { get; set; }
}

public class AccountUpdateDto
{
    public string? Name { get; set; }

    public string? City { get; set; }

    public string? StateCode { get; set; }

    public string? Country { get; set; }

    public string? EmployeesRate { get; set; }

    public double? Revenue { get; set; }

    public int? DomainId { get; set; }

    public string[] Tags { get; set; } = Array.Empty<string>();

    public Dictionary<string, string>? SocialMedia { get; set; }
}

public class AccountDetailsDto : AccountCreateDto
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

public class AccountImportDto
{
    [Optional]
    public int? Id { get; set; }

    [Optional]
    public string Name { get; set; } = string.Empty;

    [Optional]
    public string? City { get; set; }

    [Optional]
    public string? StateCode { get; set; }

    [Optional]
    public string? Country { get; set; }

    [Optional]
    public string? EmployeesRate { get; set; }

    [Optional]
    public double? Revenue { get; set; }

    [Optional]
    public int? DomainId { get; set; }

    [Optional]
    public string[] Tags { get; set; } = Array.Empty<string>();

    [Optional]
    public Dictionary<string, string>? SocialMedia { get; set; }

    [Optional]
    public DateTime? CreatedAt { get; set; }

    [Optional]
    public DateTime? UpdatedAt { get; set; }
}