﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTracker.ViewModel.Core
{
    public class BaseViewModel: ObeservableObject
    {
        public virtual void Dispose() { }
        public virtual async Task Initialize() { }
    }
}
