import { useQuery } from "@tanstack/react-query";
import { Seat } from "../types";

const url = "http://localhost:51249/api/Seat/";
export const getSeat = async (id: string) => {
  const res = await fetch(url+id);
  return res.json();
};

export const useSeat = (id: string) => {
  return useQuery<Seat>({
    queryKey: ["seat"],
    queryFn: () => getSeat(id),
  });
};
