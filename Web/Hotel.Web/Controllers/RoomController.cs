using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Data.Common.Repositories;
using Hotel.Data.Models;
using Hotel.Services.Data.Room;
using Hotel.Services.Data.User;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomsService roomsService;
        private readonly IUsersService usersService;
        private readonly IDeletableEntityRepository<Picture> picturesRepository;

        public RoomController(
            IRoomsService roomsService,
            IUsersService usersService,
            IDeletableEntityRepository<Picture> picturesRepository)
        {
            this.roomsService = roomsService;
            this.usersService = usersService;
            this.picturesRepository = picturesRepository;
        }
    }
}
