using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.Concrete
{

    public class UserImage : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Column("ImageName")]
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreateDate { get; set; }
 


    }
}