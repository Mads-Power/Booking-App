import { useQuery } from '@tanstack/react-query';

const url = '';
export const getRoom = async () => {
  const res = await fetch(url);
  return res.json();
};

export const useRoom = () => {
  return useQuery({
    queryKey: ['roomId'],
    queryFn: () => getRoom(),
  });
};
