import { useQuery } from '@tanstack/react-query';
import { Room } from '@type/room';

const url = 'http://localhost:51249/api/Room';
export const getRooms = async () => {
  const res = await fetch(url, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
    },
  });
  return res.json();
};

export const useRoomsQuery = () => {
  return useQuery<Room[]>({
    queryKey: ['rooms'],
    queryFn: () => getRooms(),
  });
};
