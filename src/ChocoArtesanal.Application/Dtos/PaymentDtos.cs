namespace ChocoArtesanal.Application.Dtos;

public record PaymentRequestDto(decimal Amount, string CardNumber, string ExpiryDate, string Cvv);

public record PaymentResponseDto(bool Success, string TransactionId, string Message);