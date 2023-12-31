﻿using N5.Challenge.Application.Abstractions.Messaging;
using N5.Challenge.Application.Permissions.Shared;

namespace N5.Challenge.Application.Permissions.GetPermissions;

public sealed record GetPermissionsQuery : IQuery<IReadOnlyList<PermissionResponse>>;