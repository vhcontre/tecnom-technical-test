using FluentValidation;


public class LeadDtoValidator : AbstractValidator<LeadDto>
{
    public LeadDtoValidator()
    {
        RuleFor(x => x.PlaceId)
            .GreaterThan(0).WithMessage("El place_id debe ser mayor a 0.");

        RuleFor(x => x.AppointmentAt)
            .GreaterThan(DateTime.MinValue).WithMessage("La fecha de appointment_at es obligatoria.");

        RuleFor(x => x.ServiceType)
            .NotEmpty().WithMessage("El service_type es obligatorio.");

        RuleFor(x => x.Contact)
            .NotNull().WithMessage("El contacto es obligatorio.")
            .SetValidator(new ContactDtoValidator());

        When(x => x.Vehicle != null, () =>
        {
            RuleFor(x => x.Vehicle).SetValidator(new VehicleDtoValidator());
        });
    }
}

public class ContactDtoValidator : AbstractValidator<ContactDto>
{
    public ContactDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("El email es obligatorio y debe ser válido.");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("El teléfono es obligatorio.");
    }
}

public class VehicleDtoValidator : AbstractValidator<VehicleDto>
{
    public VehicleDtoValidator()
    {
        RuleFor(x => x.Make).NotEmpty().WithMessage("La marca es obligatoria.");
        RuleFor(x => x.Model).NotEmpty().WithMessage("El modelo es obligatorio.");
        RuleFor(x => x.Year).GreaterThan(1900).WithMessage("El año debe ser válido.");
        RuleFor(x => x.LicensePlate).NotEmpty().WithMessage("La patente es obligatoria.");
    }
}
