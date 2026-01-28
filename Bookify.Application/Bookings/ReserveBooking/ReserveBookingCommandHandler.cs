using System;
using System.Threading;
using System.Threading.Tasks;
using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Apartments.Repository;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Repository;
using Bookify.Domain.Users;
using Bookify.Domain.Users.Repository;

namespace Bookify.Application.Bookings.ReserveBooking;

internal sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IApartmentRepository _apartmentRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PricingService _pricingService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ReserveBookingCommandHandler(
        IUserRepository userRepository, 
        IApartmentRepository apartmentRepository, 
        IBookingRepository bookingRepository, 
        IUnitOfWork unitOfWork, 
        PricingService pricingService)
    {
        _userRepository = userRepository;
        _apartmentRepository = apartmentRepository;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
        _pricingService = pricingService;
    }

    public async Task<Result<Guid>> Handle(
        ReserveBookingCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var apartment = await _apartmentRepository.GetAsync(request.ApartmentId, cancellationToken);

        if (apartment is null)
        {
            return Result.Failure<Guid>(ApartmentErrors.NotFound);
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);

        if (await _bookingRepository.IsOverlappingAsync(apartment, duration, cancellationToken))
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }

        var booking = Booking.Reserve(
            apartment,
            user.Id,
            duration,
            _dateTimeProvider.UtcNow,
            _pricingService);

        _bookingRepository.Add(booking.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return booking.Value.Id;
    }
}