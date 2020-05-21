using System;
using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class EventSubType
    {
        public Guid Id { get; set; }
        public SubTypeEnum Name { get; set; }
        
        public Guid TypeParentId { get; set; }
        public virtual EventType TypeParent { get; set; }
    }

    public enum SubTypeEnum
    {
        Rock,
        Classic,
        Blues,
        Pop,
        Jazz,
        Heavy,
        Festival,
        Punk,
        HipHop,
        Metal,
        Opera,
        Theatre,
        Musical,
        Cinema,
        Dance,
        Comedy,
        Football,
        Basketball,
        Tennis,
        OtherSports,
        Circus,
        Children,
        Clubbing, 
        Expositions,
        Proms,
        Religious
    }
}