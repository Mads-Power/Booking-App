import { useMutation, useQuery } from '@tanstack/react-query';
import { Seat } from '../types';

const url = 'http://localhost:51249/api/Seat/';
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
