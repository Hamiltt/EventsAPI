namespace Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public int MaxParticipants { get; set; }
        public List<Participant> Participants { get; set; }
        public byte[] Image { get; set; }

        public Event(string name, int maxParticipants)
        {
            Name = name;
            MaxParticipants = maxParticipants;
            Participants = new List<Participant>();
        }

        public bool CanRegisterParticipant()
        {
            return Participants.Count < MaxParticipants;
        }

        public void AddParticipant(Participant participant)
        {
            if (CanRegisterParticipant())
            {
                Participants.Add(participant);
            }
        }

        public void RemoveParticipant(Participant participant)
        {
            Participants.Remove(participant);
        }
    }
}