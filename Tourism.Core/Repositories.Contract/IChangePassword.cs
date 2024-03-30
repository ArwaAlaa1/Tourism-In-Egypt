﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
    public interface IChangePassword: IGenericRepository<ResetPassword>
    {
        Task<ResetPassword> GetPasswordofOTP(int otp , string email);
    }
}
