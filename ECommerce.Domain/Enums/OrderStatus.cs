namespace ECommerce.Domain.Enums;

public class OrderStatus
{
	public enum OrderStatus
	{
		pending = 0;
		confirmed = 1;
		shipped = 2; 
		delivered = 3;
		cancelled = 4;
	}
}
