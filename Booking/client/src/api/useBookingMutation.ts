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
  await fetch(url, requestOptions).then((res) => {
    if(res.ok) return res.json();
    let error = new Error("Http status code: " + res.status);
    error.cause = res.statusText;
    throw error;
  })
};

export const useBookingMutation = () => {
  let seatId: string;
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: addBooking,
    onError: (err: Error) => {
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['seat', seatId] });
    },
  });
};
