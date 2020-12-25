﻿namespace Hotel.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "Hotel";

        public const string AdministratorRoleName = "Administrator";

        public const string RequiredField = "This field is required";
        public const string NumberOfBedsRange = "The number of beds can be between 1 and 10";
        public const string UserNameMaxLength = "The field can contain no more than 20 characters";
        public const string CountOfPeopleInRoomLength = "The number must be between 1 and 10.";
        public const string CheckDateTimeAttribute = "Please enter date after today";
        public const string ContentMessageMaxLength = "The field can contain no more than 300 characters";



        public const string InvalidOperationExceptionInPictureService = "Exception happened in PictureService while saving the Picture in IDeletableEntityRepository<Picture>";
        public const string InvalidOperationExceptionForRoomDelete = "Exception happened in RoomsService while deleting room from IDeletableEntityRepository<Room>";

        public const string InvalidOperationExceptionForRoomEdit = "Exception happened in RoomsService while editing room in IDeletableEntityRepository<Room>";

        public const string InvalidOperationExceptionForRoomGetAllReservations = "Exception happened in RoomsService while getting all reservations for current user from IDeletableEntityRepository<RoomReservations>";

        public const string InvalidOperationExceptionForRoomGetAllReservationsForAdmin = "Exception happened in RoomsService while getting all reservations for admin from IDeletableEntityRepository<RoomReservations>";

        public const string InvalidOperationExceptionForRoomSearchForEdit = "Exception happened in RoomsService search for room in IDeletableEntityRepository<Room>";

        public const string InvalidOperationExceptionForRoomReservation = "Exception happened in RoomsService while saving the Reservation in IDeletableEntityRepository<RoomReservation>";

        public const string InvalidOperationExceptionForRoomCreate = "Exception happened in RoomsService while creating room in IDeletableEntityRepository<Room>";
    }
}
