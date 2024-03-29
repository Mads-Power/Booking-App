import { useQuery } from '@tanstack/react-query';
import { Room } from '@type/room';
const API_URL = import.meta.env.VITE_API_URL

const url = `/api/Room`;
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
