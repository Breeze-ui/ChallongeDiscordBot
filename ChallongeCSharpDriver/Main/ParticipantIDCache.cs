﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ChallongeCSharpDriver.Core.Results;

namespace ChallongeCSharpDriver.Main
{
    public class ParticipantIDCache
    {
        private ParticipantIDCache() { }

        private static ParticipantIDCache _me { get; } = new ParticipantIDCache();

        public static ParticipantIDCache Instance => _me;

        /// <summary>
        /// From ParticipantID and ParticipantGroupID to unique ParticipantID
        /// </summary>
        private Dictionary<int, int> participantDictionary { get; } = new Dictionary<int, int>();

        public void PopulateCache(params ParticipantResult[] participants)
        {
            foreach (ParticipantResult participantResult in participants)
            {
                AddToCacheIfNotExist(participantResult.id, participantResult.group_player_ids.FirstOrDefault());
            }
        }

        public void PopulateCache(params IParticipant[] participants)
        {
            foreach (IParticipant participant in participants)
            {
                AddToCacheIfNotExist(participant.id.ID, participant.id.GroupID);
            }
        }

        private void AddToCacheIfNotExist(int id, int? groupId)
        {
            if (id > 0 && !participantDictionary.ContainsKey(id))
                participantDictionary.Add(id, id);
            if (groupId.HasValue && groupId.Value > 0 && !participantDictionary.ContainsKey(groupId.Value))
                participantDictionary.Add(groupId.Value, id);
        }

        public int? GetParticipantID(ParticipantID id)
        {
            return GetParticipantID(id.ID) ?? GetParticipantID(id.GroupID);
        }

        public int? GetParticipantID(int id)
        {
            if (participantDictionary.ContainsKey(id))
                return participantDictionary[id];
            return null;
        }
    }
}
