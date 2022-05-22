import React from 'react'
import { useNavigate } from "react-router-dom";

export default function Main(props) {
    let navigate = useNavigate();

    if (props.isLoading) {
        return (
            <div>loading...</div>
        )
    }
    else {
        if (props.isAuth) {
            navigate("home", { replace: true })
        }
        else {
            navigate("login", { replace: true })
        }
    }
}
