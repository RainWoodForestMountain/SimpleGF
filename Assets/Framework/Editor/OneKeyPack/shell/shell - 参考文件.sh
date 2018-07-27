#!/bin/bash
#工程绝对路径
project_path=/Users/imac/Documents/bwgame/gc2.0/Build/ios/com.hjhd.bwgame-2.0.0.424
#工程名 将XXX替换成自己的工程名
project_name=BWGame
#plist文件所在路径
exportOptionsPath=/Users/imac/Documents/bwgame/gc2.0/Build/ios/shellHandle/exportOptions.plist
#导出.ipa文件所在路径
exportIpaPath=/Users/imac/Documents/bwgame/gc2.0/Build/ios/ipaPackages
#build出的.app文件夹路径
build_path=/Users/imac/Documents/bwgame/gc2.0/Build/ios/appPackages
#重命名后的ipa文件名
package_name=com.hjhd.bwgame-2.0.0.424
#打包模式 Debug/Release
development_mode=Debug
#scheme名 将XXX替换成自己的sheme名
scheme_name=Unity-iPhone

cd $project_path
pwd
echo '///-----------'
echo '/// 正在清理工程'
echo '///-----------'
xcodebuild -project ${project_path}/${scheme_name}.xcodeproj -target ${scheme_name} clean -quiet  || exit
echo '///--------'
echo '/// 清理完成'
echo '///--------'
echo ''
echo '///-----------'
echo '/// 正在编译工程:'
echo '///-----------'
xcodebuild archive -project ${project_path}/${scheme_name}.xcodeproj -scheme ${scheme_name} -configuration ${development_mode} -archivePath ${build_path}/${scheme_name}.xcarchive -quiet || exit
echo '///--------'
echo '/// 编译完成 开始ipa打包，请等待'
echo '///--------'
echo ''

#rm -rf ${exportIpaPath}/Payload
#rm -rf ${exportIpaPath}/${package_name}.ipa
#mkdir ${exportIpaPath}/Payload
#cp -r ${build_path}/${project_name}.app ${exportIpaPath}/Payload/${project_name}.app
#zip -r ${exportIpaPath}/${package_name}.ipa ${exportIpaPath}/Payload
#rm -rf ${exportIpaPath}/Payload

xcodebuild -exportArchive -archivePath ${build_path}/${scheme_name}.xcarchive -configuration ${development_mode} -exportPath ${exportIpaPath} -exportOptionsPlist ${exportOptionsPath} -quiet || exit
if [ -e $exportIpaPath/$scheme_name.ipa ];
    then
    rm -rf $exportOptionsPath/$package_name
    mkdir $exportOptionsPath/$package_name
    mv $exportIpaPath/$scheme_name.ipa $exportIpaPath/$/$package_name.ipa
else
    echo '///-----------'
    echo '/// failed!'
    echo '///-----------'
fi
open ${exportIpaPath}
exit 0