import { useQuery } from '@tanstack/react-query';
import { Booking } from '@type/booking';


export const getBookingsByRoom = async (roomId: number, date: Date) => {
  const url = `http://localhost:51249/api/Room/${roomId}/Bookings?date=`+encodeURI(date.toISOString())
  const requestOptions = {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
  };
  const res = await fetch(url, requestOptions);
  return res.json();
};

export const useBookingsByRoom = (roomId: number, date: Date) => {
  return useQuery<Booking[]>({
    queryKey: ['bookingsByRoom', roomId],
    queryFn: () => getBookingsByRoom(roomId, date),
    enabled: !!date
  });
};
