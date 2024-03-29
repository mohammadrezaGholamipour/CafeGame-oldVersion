﻿using CafeGameApi.Config;
using System.ComponentModel;

namespace CafeGameApi.Models;

public class LoginResponse
{
    [DisplayName("وضعیت کاربر")]
    public UserState UserState { get; set; }

    [DisplayName("توکن JWT")]
    public string? Token { get; set; }
}