﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DragonRun
{
    public class Leaderboard
    {
        [Key]
        public Guid Id { get; set; }
        public int Score { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    } 
}
