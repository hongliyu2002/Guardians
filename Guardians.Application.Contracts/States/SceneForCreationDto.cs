﻿using JetBrains.Annotations;

namespace Guardians.Application.Contracts.States;

[PublicAPI]
[Serializable]
public sealed class SceneForCreationDto
{
    public string Title { get; set; } = default!;
}