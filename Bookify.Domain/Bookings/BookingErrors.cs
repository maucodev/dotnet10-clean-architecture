using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings;

public static class BookingErrors
{
    public static Error NotFound = new(
        Code: "Booking.NotFound",
        Name: "The booking with the specified identifier was not found");

    public static Error Overlap = new(
        Code: "Booking.Overlap",
        Name: "The current booking is overlapping with an existing one");

    public static Error NotReserved = new(
        Code: "Booking.NotReserved",
        Name: "The booking is not reserved");

    public static Error NotConfirmed = new(
        Code: "Booking.NotConfirmed",
        Name: "The booking is not confirmed");

    public static Error AlreadyStarted = new(
        Code: "Booking.AlreadyStarted",
        Name: "The booking has already started");
}