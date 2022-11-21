import { useMutation } from '@tanstack/react-query';

type UpdateSeat = {
  data: {
    available: string;
    userId: string;
    date: string;
  };
  seatId: number;
};

const url = '';
export const updateSeat = async ({ data, seatId }: UpdateSeat) => {
  const res = await fetch(`${url}/${seatId}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });
  return res.json();
};

export const useUpdateSeat = () => {
  return useMutation({
    mutationFn: updateSeat,
  });
};
