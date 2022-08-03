using System.Collections.Generic;

namespace PartyInvites.Models;

public static class Repository
{
    private static readonly List<GuestResponse> responses = new();

    public static IEnumerable<GuestResponse> Responses { get { return responses; } }

    public static void AddResponse(GuestResponse response)
    {
        responses.Add(response);
    }
}
