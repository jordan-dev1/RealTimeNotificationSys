using Core.Entities;
using System.ComponentModel.DataAnnotations;

    namespace Core.Entities
    {
        public class Notification
        {
            public int ID { get; set; }
        [Required]
        [MaxLength(255)]
        public string Message { get; set; }

        public int ChannelId { get; set; }
        public Channel Channel { get; set; }
   
         }
    }


