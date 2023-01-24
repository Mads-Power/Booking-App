import { useMutation } from '@tanstack/react-query';
import { DeleteBooking } from '@type/booking';


const url = 'http://localhost:51249/api/Booking/Unbook';
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
  await fetch(url, requestOptions).then(res => {
    if(res.ok) return res
    let error = new Error("Http status code: " + res.status);
    error.cause = res.statusText;
    throw error;
  });
};

export const useRemoveBookingMutation = () => {
  return useMutation({
    mutationFn: removeBooking,
    onError: error => {
    },
    onMutate: m => {
      console.log(m);
    },
    onSuccess: s => {
      console.log(s);
    },
  });
};
