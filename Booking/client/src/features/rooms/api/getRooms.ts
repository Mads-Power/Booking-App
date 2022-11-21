import { useQuery } from '@tanstack/react-query';

const url = '';
export const getRooms = async () => {
  const res = await fetch(url);
  return res.json();
};

export const useRooms = () => {
  return useQuery({
    queryKey: ['rooms'],
    queryFn: () => getRooms(),
  });
};
