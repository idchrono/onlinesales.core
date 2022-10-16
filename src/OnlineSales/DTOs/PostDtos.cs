﻿// <copyright file="PostDtos.cs" company="WavePoint Co. Ltd.">
// Licensed under the MIT license. See LICENSE file in the samples root for full license information.
// </copyright>
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineSales.DTOs;

public class PostCreateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    public string CoverImageUrl { get; set; } = string.Empty;

    public string CoverImageAlt { get; set; } = string.Empty;

    [Required]
    public string Slug { get; set; } = string.Empty;

    [Required]
    public string Template { get; set; } = string.Empty;

    [Required]
    public string Author { get; set; } = string.Empty;

    [Required]
    public string Lang { get; set; } = string.Empty;

    public string Categories { get; set; } = string.Empty;

    public string Tags { get; set; } = string.Empty;

    public bool AllowComments { get; set; } = false;
}

public class PostUpdateDto
{
    public string? Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public string? Content { get; set; } = string.Empty;

    public string? CoverImageUrl { get; set; } = string.Empty;

    public string? CoverImageAlt { get; set; } = string.Empty;

    public string? Slug { get; set; } = string.Empty;

    public string? Template { get; set; } = string.Empty;

    public string? Author { get; set; } = string.Empty;

    public string? Lang { get; set; } = string.Empty;

    public string? Categories { get; set; } = string.Empty;

    public string? Tags { get; set; } = string.Empty;

    public bool? AllowComments { get; set; } = false;
}