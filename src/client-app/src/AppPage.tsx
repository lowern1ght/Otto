import {Typography} from "antd";
import style from "./styles/AppPage.module.css"

export interface IAppPageProp {
    title : string
}

export const AppPage = ({title} : IAppPageProp) => {
    return (
        <div className={`${style._main}`}>
            <Typography.Title>
                {title}
            </Typography.Title>
        </div>
    );
};