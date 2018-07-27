#使用方法

if [ ! -d ./shellHandle ];
    then
        mkdir -p ./shellHandle;
fi


if [ ! -d ./shellHandle/IPADir ];
    then
        mkdir -p ./shellHandle/IPADir;
fi



arr=();

#打包成功的个数
success=0;

#打包失败的个数
fail=0;


#工程绝对路径
project_path=$(cd `dirname $0`; pwd)

#工程名 将XXX替换成自己的工程名
project_name=XXXXXXX

#scheme名 将XXX替换成自己的sheme名
scheme_name=XXXXXXX

#打包模式 Debug/Release
development_mode=Release

#build文件夹路径
build_path=${project_path}/shellHandle/build

#plist文件所在路径
exportOptionsPlistPath=${project_path}/shellHandle/exportOptionsPlist.plist

#导出.ipa文件所在路径
exportIpaPath=${project_path}/shellHandle/IPADir/${development_mode}

echo "确认是否为release 环境？"
read number1
echo "服务器是否为正式环境？"
read number2
echo "是否关闭log日志？"
read number3
echo "[ 1:是 2:否] "

enterNum=1;

##
read number
    while([[ $number != 1 ]] && [[ $number != 2 ]])
    do
        if [ $enterNum == 5 ];
            then
        echo "输入多次错误退出！！"
        exit 0
        fi
        echo "Error! Should enter 1 or 2"
        echo "确认是否为release 环境？"
        echo "服务器是否为证实环境？"
        echo "是否关闭log日志？"
        echo "[ 1:是 2:去确认] "
        enterNum=$[$enterNum+1]
        read number
    done

if [ $number == 1 ];
    then 
    echo "开始批量打包"
    startTime=$(date +%c)
else
    echo "请确认打包环境再进行打包"
    exit 0
fi



# 循环进入

for file2 in `ls -a ./shellHandle/module`
do
    if [ x"$file2" != x"." -a x"$file2" != x".." -a x"$file2" != x".DS_Store" ]; then
        echo $file2

        #Conf file
        CONF=./shellHandle/module/$file2/resign.conf

        echo $CONF


        #Datetime
        NOW=$(date +"%Y%m%d_%s")
        Appcode1="121212121"
        Appcode2="121212121"
        appViewaon="2.03"


        #Load config
        if [ -f ${CONF} ]; then
            . ${CONF}
        fi

        #Temp
        TEMP="temp"
        if [ -e ${TEMP} ]; then
            echo "ERROR: temp already exists"
            exit 1
        fi



        #Check app ID
        if [ -z ${APP_ID} ]; then
            echo "ERROR: missing APP_ID"
            exit 1
        fi

        echo ${APP_ID}

        
        if [ -z ${APP_NAME} ]; then
            echo "ERROR: missing APP_NAME"
            exit 1
        fi

        echo ${APP_NAME}
        
        if [ -z ${APP_SCHOOLCODE} ]; then
            echo "ERROR: missing APP_SCHOOLCODE"
            exit 1
        fi
        echo ${APP_SCHOOLCODE}


        if [ -z ${APP_SCHOOLCODE2} ]; then
            echo "ERROR: missing APP_SCHOOLCODE2"
            exit 1
        fi
        echo ${APP_SCHOOLCODE2}


        #修改icon的目录文件
        APPICONTemp="${project_path}/XXXXXXXX/Assets.xcassets/AppIcon.appiconset"
        
        #修改plist文件的目录
        APPICONplist="${project_path}/XXXXXXXX"


        echo ${APPICONplist}/Info.plist

        # Change icon
        echo "Change icon"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPad-40@2x.png" "${APPICONTemp}/Icon-iPad-40@2x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPad-40.png" "${APPICONTemp}/Icon-iPad-40.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPad-29@2x.png" "${APPICONTemp}/Icon-iPad-29@2x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPad-29.png" "${APPICONTemp}/Icon-iPad-29.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPad-50@2x.png" "${APPICONTemp}/Icon-iPad-50@2x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPad-50.png" "${APPICONTemp}/Icon-iPad-50.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPad-72@2x.png" "${APPICONTemp}/Icon-iPad-72@2x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPad-72.png" "${APPICONTemp}/Icon-iPad-72.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPad-76@2x.png" "${APPICONTemp}/Icon-iPad-76@2x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPad-76.png" "${APPICONTemp}/Icon-iPad-76.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPhone-57.png" "${APPICONTemp}/Icon-iPhone-57.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPhone-57@2x.png" "${APPICONTemp}/Icon-iPhone-57@2x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPhone-60@2x.png" "${APPICONTemp}/Icon-iPhone-60@2x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPhone-60@3x.png" "${APPICONTemp}/Icon-iPhone-60@3x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPhone-29@3x.png" "${APPICONTemp}/Icon-iPhone-29@3x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPhone-29@2x.png" "${APPICONTemp}/Icon-iPhone-29@2x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPhone-29.png" "${APPICONTemp}/Icon-iPhone-29.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPhone-40@3x.png" "${APPICONTemp}/Icon-iPhone-40@3x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPhone-40@2x.png" "${APPICONTemp}/Icon-iPhone-40@2x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPhone-20@3x.png" "${APPICONTemp}/Icon-iPhone-20@3x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPhone-20@2x.png" "${APPICONTemp}/Icon-iPhone-20@2x.png"
        cp "./shellHandle/module/$file2${ASSETS_PATH}/Icon-iPad-83.5@2x.png" "${APPICONTemp}/Icon-iPad-83.5@2x.png"

        # cp "./module/$file2${ASSETS_PATH}/AppIcon40x40@2x.png" "${TEMP}/Payload/${APP_NAME}.app/AppIcon40@2x.png"
        # cp "./module/$file2${ASSETS_PATH}/AppIcon40x40@3x.png" "${TEMP}/Payload/${APP_NAME}.app/AppIcon40@3x.png"
        # cp "./module/$file2${ASSETS_PATH}/AppIcon57x57.png" "${TEMP}/Payload/${APP_NAME}.app/AppIcon29x29.png"
        echo '///-----------'
        echo '/// icon 替换完成'
        echo '///-----------'




        # #Change Bundleversion
        # if [[ ! -z ${APP_BUNDLE_VERSION} ]]; then
        #     /usr/libexec/PlistBuddy -c "Set CFBundleVersion ${APP_BUNDLE_VERSION}" ${TEMP}/Payload/${APP_NAME}.app/Info.plist
        # fi


        # #Change CFBundleShortVersionString
        # if [[ ! -z ${APP_BUNDLE_SHORT_VERSION_STRING} ]]; then
        #     /usr/libexec/PlistBuddy -c "Set CFBundleShortVersionString ${APP_BUNDLE_SHORT_VERSION_STRING}" ${TEMP}/Payload/${APP_NAME}.app/Info.plist
        # fi


        if [[ ! -z ${App_DisapleName} ]]; then
            /usr/libexec/PlistBuddy -c "Set CFBundleDisplayName ${App_DisapleName}" ${APPICONplist}/Info.plist
        fi
        # #Change CFBundleShortVersionString
        # if [[ ! -z ${APP_BUNDLE_SHORT_VERSION_STRING} ]]; then
        #     /usr/libexec/PlistBuddy -c "Set CFBundleShortVersionString ${appViewaon}" ${TEMP}/Payload/${APP_NAME}.app/Info.plist
        # fi

        # #Change Bundleidentifier
        # /usr/libexec/PlistBuddy -c "Set Bundle display name ${APP_NAME}" ${TEMP}/Payload/${APP_NAME}.app/Info.plist

        /usr/libexec/PlistBuddy -c "Set school_code ${APP_SCHOOLCODE}" ${APPICONplist}/Info.plist

        /usr/libexec/PlistBuddy -c "Set school_code2 ${appcode1}${APP_SCHOOLCODE2}${appcode2}" ${APPICONplist}/Info.plist

        echo '///-----------'
        echo '/// plist 文件修改完成'
        echo '///-----------'

        #/usr/libexec/PlistBuddy -c "Set school_message ${APP_SCHOOLMESSAGE}" ${TEMP}/Payload/${APP_NAME}.app/Info.plist
        echo '///-----------'
        echo '/// 正在清理工程'
        echo '///-----------'
        xcodebuild \
        clean -configuration ${development_mode} -quiet  || exit

        echo '///--------'
        echo '/// 清理完成'
        echo '///--------'
        echo ''

        echo '///-----------'
        echo '/// 正在编译工程:'${development_mode}
        echo '///-----------'
        xcodebuild \
        archive -workspace ${project_path}/${project_name}.xcworkspace \
        -scheme ${scheme_name} \
        -configuration ${development_mode} \
        -archivePath ${build_path}/${project_name}.xcarchive -quiet  || exit

        echo '///--------'
        echo '/// 编译完成'
        echo '///--------'
        echo ''

        echo '///----------'
        echo '/// 开始ipa打包'
        echo '///----------'

        xcodebuild -exportArchive -archivePath ${build_path}/${project_name}.xcarchive \
        -configuration ${development_mode} \
        -exportPath ${exportIpaPath} \
        -exportOptionsPlist ${exportOptionsPlistPath} \
        -quiet || exit

        if [ -e $exportIpaPath/$scheme_name.ipa ];
            then
            echo '///----------'
            echo '/// ipa包已导出'
            echo '///----------'
            mv $exportIpaPath/$scheme_name.ipa ${project_path}/shellHandle/IPADir/build/${APP_SCHOOLCODE}.ipa
            success=$[$success+1];

    # open $exportIpaPath
        else
            echo '///-------------'
            echo '/// ipa包导出失败${APP_SCHOOLCODE}'
            echo '///-------------'
            fail=$[$fail+1]

        fi
        echo '///------------'
        echo '/// 打包ipa完成  '
        echo '///-----------='
        echo ''
    fi
done

endTime=$(date +%c)
echo "开始时间:${startTime}"
echo "结束时间:${endTime}"
echo "成功=${success} 失败=${fail}"
echo "批量打包ipa完成>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>"
exit 0