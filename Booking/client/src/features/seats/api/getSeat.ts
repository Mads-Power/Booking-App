import { useQuery } from "@tanstack/react-query";

const url = "";
export const getSeat = async () => {
  const res = await fetch(url);
  return res.json();
};

export const useSeat = () => {
  return useQuery({
    queryKey: ["seat"],
    queryFn: () => getSeat(),
  });
};
