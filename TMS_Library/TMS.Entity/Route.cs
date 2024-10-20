using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_Library.TMS.Entity
{
        public class Route
        {
            private int routeID;
            private string startDestination;
            private string endDestination;
            private decimal distance;

            // Default Constructor
            public Route() { }

            // Parameterized Constructor
            public Route(int routeID, string startDestination, string endDestination, decimal distance)
            {
                this.routeID = routeID;
                this.startDestination = startDestination;
                this.endDestination = endDestination;
                this.distance = distance;
            }

            // Getters and Setters
            public int RouteID
            {
                get => routeID;
                set => routeID = value;
            }

            public string StartDestination
            {
                get => startDestination;
                set => startDestination = value;
            }

            public string EndDestination
            {
                get => endDestination;
                set => endDestination = value;
            }

            public decimal Distance
            {
                get => distance;
                set => distance = value;
            }
        }
    }
