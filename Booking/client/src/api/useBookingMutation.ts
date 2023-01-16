import { useMutation, useQueryClient } from '@tanstack/react-query';

export type CreateBooking = {
  seatId: number;
  userId: number;
  date: string;
};

const url = 'http://localhost:51249/api/Booking/Book';
export const addBooking = async ({ seatId, userId, date }: CreateBooking) => {
  const requestOptions = {
    method: 'PUT',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      seatId,
      userId,
      date,
    }),
  };
  const res = await fetch(url, requestOptions);

  return res.json();
};

export const useBookingMutation = () => {
  let seatId: string;
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: addBooking,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['seat', seatId] });
    },
  });
};
