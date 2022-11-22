import { useQuery } from '@tanstack/react-query';

const url = '';
export const getUser = async () => {
  const res = await fetch(url);
  return res.json();
};

export const useUser = () => {
  return useQuery({
    queryKey: ['userId'],
    queryFn: () => getUser(),
  });
};
