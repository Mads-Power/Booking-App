import { useMutation, useQuery } from '@tanstack/react-query';
import { Seat } from '@type/seat';
const API_URL = import.meta.env.VITE_API_URL

const url = `/api/Seat/`;
export const getSeat = async (id: string) => {
  const requestOptions = {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
  };
  const res = await fetch(url + id, requestOptions);
  return res.json();
};

export const useSeat = (id: string) => {
  return useQuery<Seat>({
    queryKey: ['seat'],
    queryFn: () => getSeat(id),
  });
};
