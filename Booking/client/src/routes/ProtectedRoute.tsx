import { userAtom } from "@components/Provider/app";
import { User } from "@type/user";
import { useAtom } from "jotai";
import { useEffect } from "react";
import { Navigate, useNavigate } from "react-router-dom";

type IProtectedRoute = {
    user: User | undefined;
    outlet: JSX.Element
}

export const ProtectedRoute = ({ user, outlet }: IProtectedRoute) => {

    useEffect(() => {
        if(!user) return;
    }, [])

    const [loggedInUser, setLoggedInUser] = useAtom(userAtom)

    const navigateToPage = (page: string) => {
        page = '/' + page
        return <Navigate to={page} replace />;
    }
    const userSessionObjectValue = window.sessionStorage.getItem('user');
    
    if (!user) {
            if(!userSessionObjectValue) return navigateToPage('login');

            const path = window.location.href.split('/').slice(3);
            if(path.length < 2 && path[0] === '') return navigateToPage('rooms');
            const newPath = (path.length > 1) ? path.join('/')  : path.join();
            setLoggedInUser(JSON.parse(userSessionObjectValue));
            return navigateToPage(newPath);
        } else {
            return outlet
        }
  };