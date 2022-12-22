import { useQuery } from '@tanstack/react-query';
import { Room } from '../types';

const url = 'http://localhost:51249/api/Room';
export const getRooms = async () => {
  const res = await fetch(url, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
    },
  });
  console.log(res);
  return res.json();
};

export const useRooms = () => {
  return useQuery<Room[]>({
    queryKey: ['rooms'],
    queryFn: () => getRooms(),
  });
};
