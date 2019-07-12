using ManheimData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManheimData
{
    public static class SyncUtils
    {
        public static List<VehicleModel> RelateVehiclesWithEvents(List<VehicleModel> adesaVehiclesMapped, List<EventModel> savedEvents)
        {
            var vehicleWithEvent = new List<VehicleModel>();
            foreach (var item in adesaVehiclesMapped)
            {
                var result = savedEvents.Where(x => x.ExternalIds.Contains(item.ExternalId));

                if (result.Any() && !string.IsNullOrEmpty(result.FirstOrDefault().Id))
                {
                    // Take the firts event related by ExternalId to set HasVehicles
                    var eventToUpdate = result.FirstOrDefault();
                    if (eventToUpdate.HasVehicles == 0)
                    {
                        eventToUpdate.HasVehicles = 1;
                        //await _repository.PutSingle(eventToUpdate);
                    }

                    // Relationship of Vehicle with an Event
                    item.Event = result.FirstOrDefault().Id;
                    item.EventName = result.FirstOrDefault().Name;
                    vehicleWithEvent.Add(item);
                }
            }

            return vehicleWithEvent;
        }
    }
}
