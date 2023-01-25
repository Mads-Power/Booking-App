import { useQuery } from '@tanstack/react-query';
import { Seat } from '@type/seat';

const url = '/api/Seat/';
export const getSeat = async (seatId: string) => {
  const requestOptions = {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
  };
  const res = await fetch(url + seatId, requestOptions);
  return res.json();
};

export const useSeatQuery = (seatId: string) => {
  return useQuery<Seat>({
    queryKey: ['seat', seatId],
    queryFn: () => getSeat(seatId),
    refetchInterval: 1000,
  });
};
