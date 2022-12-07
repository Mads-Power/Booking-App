import { useQuery } from '@tanstack/react-query';

const url = 'http://localhost:51249/api/Room';
export const getRooms = async () => {
  const res = await fetch(url, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
    },
  });
  console.log(res.json);
  return res.json();
};

export const useRooms = () => {
  return useQuery({
    queryKey: ['rooms'],
    queryFn: () => getRooms(),
  });
};
