import { useQuery } from "@tanstack/react-query";
export type Auth = {
  id: number;
};

const url = "http://localhost:51249/api/";
export const getAuth = async (id: number) => {
  const requestOptions = {
    method: "GET",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
  };
  const res = await fetch(url + id, requestOptions);
  return res.json();
};

export const useSeatQuery = (id: number) => {
  return useQuery<Auth>({
    queryKey: ["authId", id],
    queryFn: () => getAuth(id),
    refetchInterval: 1000,
  });
};
