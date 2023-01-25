import { useMutation } from '@tanstack/react-query';

export type DeleteBooking = {
  userId: number;
  date: string;
};

const url = `/api/Booking/Unbook`;
export const removeBooking = async ({ userId, date }: DeleteBooking) => {
  const requestOptions = {
    method: 'PUT',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      userId,
      date,
    }),
  };
  const res = await fetch(url, requestOptions);

  // return as json if error, so we can log error message
  if (res.status == 400) {
    return res.json();
  } else {
    return res;
  }
};

export const useRemoveBookingMutation = () => {
  return useMutation({
    mutationFn: removeBooking,
    onError: error => {
      console.log(error);
    },
    onMutate: m => {
      console.log(m);
    },
    onSuccess: s => {
      console.log(s);
    },
  });
};
