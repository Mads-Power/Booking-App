import { useMutation, useQueryClient } from '@tanstack/react-query';

export type CreateBooking = {
  seatId: number;
  email: string;
  date: string;
};

const url = '/api/Booking/Book';
export const addBooking = async ({ seatId, email, date }: CreateBooking) => {
  const requestOptions = {
    method: 'PUT',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      seatId,
      email,
      date,
    }),
  };
  await fetch(url, requestOptions).then((res) => {
    if(res.ok) return res
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
