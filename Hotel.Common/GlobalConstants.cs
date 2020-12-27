namespace Hotel.Common
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
        public const string ConfHallReserveGuestsMax = "The number must be between 1 and 100";
        public const string InvalidEmail = "Invailid email adress";
        public const string InvalidPhoneNumber = "Invalid phone number";
        public const string RestaurantReserveGuestsMax = "The number must be between 1 and 100";
        public const string EnterValidNumberOfGuestsError = "Maximum capacity of guests ";
        public const string EnterAtleastOneNightStandsError = "PLease enter a date with atleast one night"; 
        public const string ReserveRoomTempDataSuccess = "You successfully booked a room!";
        public const string CreateRoomTempDataSuccess = "Room created successfuly!";
        public const string DeleteRoomTempDataSuccess = "Room deleted successfuly!";
        public const string EditRoomTempDataSuccess = "Room updated successfuly!";
        public const string ContactFormTitleMaxLength = "The field must have minimum of 30 characters";
        public const string SuccessfullySentAnEmail = "You have successfully sent an email! Please check your mailbox for a reply!";
        public const string FreeSeatsForHallError = "The available seats for this date is ";
        public const string ReserveConferenceHallTempDataSuccess = "You successfully booked a conference hall!";
        public const string FreeSeatsForRestaurantError = "The available seats for this date is ";
        public const string ReserveRestaurantTempDataSuccess = "You successfully booked a restaurant!";



        public const string InvalidOperationExceptionInPictureService = "Exception happened in PictureService while saving the Picture in IDeletableEntityRepository<Picture>";
        public const string InvalidOperationExceptionForRoomDelete = "Exception happened in RoomsService while deleting room from IDeletableEntityRepository<Room>";

        public const string InvalidOperationExceptionForRoomEdit = "Exception happened in RoomsService while editing room in IDeletableEntityRepository<Room>";

        public const string InvalidOperationExceptionForRoomGetAllReservations = "Exception happened in RoomsService while getting all reservations for current user from IDeletableEntityRepository<RoomReservations>";

        public const string InvalidOperationExceptionForRoomGetAllReservationsForAdmin = "Exception happened in RoomsService while getting all reservations for admin from IDeletableEntityRepository<RoomReservations>";

        public const string InvalidOperationExceptionForRoomSearchForEdit = "Exception happened in RoomsService search for room in IDeletableEntityRepository<Room>";

        public const string InvalidOperationExceptionForRoomReservation = "Exception happened in RoomsService while saving the Reservation in IDeletableEntityRepository<RoomReservation>";

        public const string InvalidOperationExceptionForRoomCreate = "Exception happened in RoomsService while creating room in IDeletableEntityRepository<Room>";

        public const string InvalidOperationExceptionForConferenceHallGetAllReservations = "Exception happened in ConferenceHallService while getting all reservations for current user from IDeletableEntityRepository<ConferenceHallReservations>";

        public const string InvalidOperationExceptionForConferenceHallGetAllReservationsForAdmin = "Exception happened in ConferenceHallService while getting all reservations for admin IDeletableEntityRepository<ConferenceHallReservations>";
        public const string InvalidOperationExceptionForRestaurantGetAllReservations = "Exception happened in RestaurantService while getting all reservations for current user from IDeletableEntityRepository<RestaurantReservations>";
        public const string InvalidOperationExceptionForRestaurantReservation = "Exception happened in RestaurantService while saving the Reservation in IDeletableEntityRepository<RestaurantReservation>";

        public const string InvalidOperationExceptionForRestaurantGetAllReservationsForAdmin = "Exception happened in RestaurantService while getting all reservations for admin from IDeletableEntityRepository<RestaurantReservations>";
    }
}
