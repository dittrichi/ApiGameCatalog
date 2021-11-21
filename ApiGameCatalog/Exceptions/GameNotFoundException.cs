﻿using System;

namespace ApiGameCatalog.Exceptions
{
    public class GameNotFoundException : Exception
    {
        public GameNotFoundException()
            :base("Game not found")
        {

        }
    }
}
