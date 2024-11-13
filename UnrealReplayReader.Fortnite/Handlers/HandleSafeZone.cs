using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Fortnite.Models.Replay;
using UnrealReplayReader.Models;

namespace UnrealReplayReader.Fortnite.Handlers;

public class HandleSafeZone
{
    public static void OnProperties(SafeZoneIndicatorExport model, Actor actor, MatchData match, StateData states)
    {
        // Check if the necessary parameters are not null
        if (model == null)
        {
            Console.WriteLine("Error: model is null");
            return;
        }

        if (states == null)
        {
            Console.WriteLine("Error: states is null");
            return;
        }

        if (states.GameState == null)
        {
            Console.WriteLine("Error: GameState in states is null");
            return;
        }

        if (states.SafeZones == null)
        {
            Console.WriteLine("Error: SafeZones list in states is null");
            return;
        }

        if (match == null)
        {
            Console.WriteLine("Error: match is null");
            return;
        }

        if (match.SafeZones == null)
        {
            Console.WriteLine("Error: SafeZones list in match is null");
            return;
        }

        // Try to get the last safe zone, or default to null if the list is empty
        var newSafeZone = states.SafeZones.LastOrDefault();

        // Check if the SafeZoneFinishShrinkTime property is not null
        if (model.SafeZoneFinishShrinkTime != null)
        {
            if (newSafeZone != null)
            {
                newSafeZone.PlayersAlive = states.GameState.RemainingBots + states.GameState.RemainingPlayers;
            }

            // Create a new safe zone
            newSafeZone = new SafeZone();

            // Add the new safe zone to the states and match SafeZones lists
            states.SafeZones.Add(newSafeZone);
            match.SafeZones.Add(newSafeZone);

            // Set the properties of the new safe zone, using null-coalescing to avoid null values
            newSafeZone.Radius = model.Radius ?? newSafeZone.Radius;
            newSafeZone.NextNextRadius = model.NextNextRadius ?? newSafeZone.NextNextRadius;
            newSafeZone.NextRadius = model.NextRadius ?? newSafeZone.NextRadius;
            newSafeZone.ShringEndTime = model.SafeZoneFinishShrinkTime ?? newSafeZone.ShringEndTime;
            newSafeZone.ShrinkStartTime = model.SafeZoneStartShrinkTime ?? newSafeZone.ShrinkStartTime;
            newSafeZone.LastCenter = model.LastCenter ?? newSafeZone.LastCenter;
            newSafeZone.NextCenter = model.NextCenter ?? newSafeZone.NextCenter;
            newSafeZone.NextNextCenter = model.NextNextCenter ?? newSafeZone.NextNextCenter;
        }

        // Ensure newSafeZone is not null before setting its CurrentRadius
        if (newSafeZone != null)
        {
            newSafeZone.CurrentRadius = model.Radius ?? newSafeZone.CurrentRadius;
        }
        else
        {
            Console.WriteLine("Error: newSafeZone is null, unable to set CurrentRadius.");
        }
    }
}
