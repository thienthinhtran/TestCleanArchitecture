﻿using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Event
{
    public class AvailableMachine : BaseEvent
    {
        private readonly Machine _machine;
        public AvailableMachine(Machine machine)
        {
            _machine = machine;
        }
    }
}
