import { useMutation } from '@tanstack/react-query';
const API_URL = import.meta.env.VITE_API_URL

export type DeleteBooking = {
  userId: number;
  date: string;
};

const url = `${API_URL}/api/Booking/Unbook`;
export const putBookingUnbook = async ({ userId, date }: DeleteBooking) => {
  const requestOptions = {
    method: 'PUT',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      userId,
      date
    })
  };
  const res = await fetch(url, requestOptions);

  // return as json if error, so we can log error message
  if (res.status == 400) {
    return res.json();
  } else {
    return res;
  }
};

export const useUnbook = () => {
  return useMutation({
    mutationFn: putBookingUnbook,
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
